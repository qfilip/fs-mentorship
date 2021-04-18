module Database

open Dapper
open System.Data
open System.Data.SqlClient
open FSharp.Data
open FSharp.Core
open DomainModels.Common

[<CLIMutable>]
type CocktailEntity = {
    Id: string
    Name: string
    EntityStatus: eEntityStatus
}

let connString = "Data Source=\"D:\\Riznice\\fs-mentorship\\Server_Sol\\Server.Api\\WebRoot\\mentorDb.db3"

let mapRowsToRecords (reader: IDataReader): CocktailEntity list =
    // Get the indexes of each column
    let readData propName = (reader.GetOrdinal propName).ToString()

    let id = readData (nameof Unchecked.defaultof<CocktailEntity>.Id)
    let name = readData (nameof Unchecked.defaultof<CocktailEntity>.Name)
    let entityStatus = reader.GetOrdinal "EntityStatus"

    [ while reader.Read() do
        let status = 
            match reader.GetString entityStatus with
            | "0" -> eEntityStatus.Active
            | "1" -> eEntityStatus.Deleted
            | _ -> failwith "dammit"

        yield
            { Id = id
              Name = name
              EntityStatus = status } ]


let get (connStr: string): Async<CocktailEntity list> =
    let sql = "SELECT * FROM Cocktail"
    // Execute an async block which connects to the database and maps the result
    async {
        // Connect to the SQL database
        use conn = new SqlConnection(connStr)
        // Execute the SQL query and get a reader
        use! reader = conn.ExecuteReaderAsync(sql) |> Async.AwaitTask
        //use! reader = conn.QueryMultiple(sql, data) |> Async.AwaitTask
        // Map the rows to Employee records and try to get the first element of the list, converting it to an option
        return mapRowsToRecords reader
    }
