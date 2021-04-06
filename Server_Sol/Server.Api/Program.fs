﻿// Learn more about F# at http://fsharp.org

open System
open System.IO
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open ApiServices

let webApp =
    choose [
        GET >=>
            choose [
                route "/" >=> text "Hi"
                route "/hello" >=> text "Hi"

                subRoute "/api" (choose ApiEndpoints.Cocktail.gets)
            ]
            
            
        POST >=> 
            choose [
                subRoute "/api" (choose ApiEndpoints.Cocktail.posts)
            ]

            
        setStatusCode 404 >=> text "Not Found" ]

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message


let configureCors (builder : CorsPolicyBuilder) =
    builder
       .AllowAnyOrigin() //("http://localhost:5500")
       .AllowAnyMethod()
       .AllowAnyHeader()
       |> ignore


let configureApp (app : IApplicationBuilder) =
    let env = app.ApplicationServices.GetService<IWebHostEnvironment>()
    (match env.IsDevelopment() with
    | true  ->
        app.UseDeveloperExceptionPage()
    | false ->
        app.UseGiraffeErrorHandler(errorHandler)
            .UseHttpsRedirection())
        .UseCors(configureCors)
        .UseStaticFiles()
        .UseGiraffe(webApp)


let configureServices (services : IServiceCollection) =
    services.AddCors()    |> ignore
    services.AddGiraffe() |> ignore
    services.AddSingleton<DataAccess>() |> ignore


let configureLogging (builder : ILoggingBuilder) =
    builder.AddConsole()
           .AddDebug() |> ignore


[<EntryPoint>]
let main args =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "WebRoot")
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .UseContentRoot(contentRoot)
                    .UseWebRoot(webRoot)
                    .Configure(Action<IApplicationBuilder> configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                    |> ignore)
        .Build()
        .Run()
    0
