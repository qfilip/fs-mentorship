module Dal

open System
open System.IO
open Thoth.Json.Net
open ValidationCommon
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
        | Ok db -> Success (db |> f)
        | Error e -> Failure [e]


open Common
module MockDb =
    let mockit =
        let mkUser (keyNum, nick) = { Key = keyNum; Nick = nick }
        let mkId () = Guid.NewGuid()
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

