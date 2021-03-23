#r "nuget: Thoth.Json.Net, 5.0.0"
#load "simpleTypes.fsx"

open Thoth.Json.Net
open System.IO

open SimpleTypes
open SimpleTypes.String5

type Role = Admin | User | Guest

type User = {
    Name: string
    Age: int
    Role: Role
    Email: string option
    Code: String5
}

let filepath = "D:\\Riznice\\fs-mentorship\\Practice\\sample.json"


let writeToFile content =
    File.WriteAllText (filepath, content)

let readFromFile = File.ReadAllText filepath

let makeUser code =
    match code with
        | Error e -> printfn "%s" e
        | Ok validCode -> 
            let user = { Name = "Bob"; Age = 27; Role = Admin; Email = None; Code = validCode }
            let json = Encode.Auto.toString(4, user)
            writeToFile json |> ignore

            let jsonText = readFromFile
            let userOfJson = Decode.Auto.fromString<User>(jsonText)

            printfn "%A" userOfJson



let code = String5.create "IDDQD"

makeUser code


