module Dal

open System
open System.IO
open Thoth.Json.Net
open Models.Common
open Models.Tables

module Common =
    type SampleDatabase = {
        Users: UserTbl list
        Transactions: TransactionTbl list
        Wallets: WalletTbl list
    }
    
    let private filepath = Path.Combine(__SOURCE_DIRECTORY__, "evenSimplerDB.json")
    let readFromFile = File.ReadAllText filepath
    
    let saveDb content = File.WriteAllText (filepath, content)
    let loadDb = Decode.Auto.fromString<SampleDatabase>(readFromFile)

    let execute f =
        match loadDb with
        | Ok db -> Ok (db |> f)
        | Error e -> Error [e]


open Common
module MockDb =
    let mockit =
        let mkUser (keyNum, nick) = { Key = keyNum; Nick = nick }
        let mkId () = Guid.NewGuid().ToString()
        let mkWallet user coin amount: WalletTbl =
            { Id = mkId (); UserKey = user.Key; Coin = coin; Amount = amount }
        
        let users = 
            [(127, "Aziz"); (450, "Baal"); (234, "Vaana")]
            |> List.map (fun x -> mkUser x)
        
        let wallets =
            users
            |> List.map (fun x -> mkWallet x Coin.Bitcoin 100.)

        let db: SampleDatabase = {
            Users = users
            Wallets = wallets
            Transactions = []
        }

        let json = Encode.Auto.toString(4, db)
        saveDb json |> ignore

open Dapper
open Microsoft.Data.Sqlite
module SqlDb =
    //https://www.codesuji.com/2017/07/29/F-and-Dapper/
    let private dataSource = Path.Combine(__SOURCE_DIRECTORY__, "coins.db3")
    let private connectionString = sprintf "Data Source=%s;Version=3;" dataSource

    let command (operation: unit) =
        let connection = new SqliteConnection(connectionString)
        connection.Open()
        
        //let insertTradeSql = 
        //    "insert into user(key, nick) " + 
        //    "values (@key, @nick)"
        
        //let sqlCmd (items: UserTbl list) (commandString: string)=
        //    items
        //    |> List.map (fun x -> connection.Execute(commandString, x))
        //    |> List.sum
        //    |> (fun recordsAdded -> printfn "Records added  : %d" recordsAdded)
        
        connection.Close()

