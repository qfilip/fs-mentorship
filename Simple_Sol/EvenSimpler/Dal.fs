module Dal

open System.IO
open Models.Tables

module Common =
    type SampleDatabase = {
        Users: UserTbl list
        Transactions: TransactionTbl list
        Wallets: WalletTbl list
    }
    let private filepath = Path.Combine(__SOURCE_DIRECTORY__, "evenSimplerDB.json")
    let writeToFile content = File.WriteAllText (filepath, content)
    let readFromFile = File.ReadAllText filepath


open System
open Common
open Models.Common
open Thoth.Json.Net

module MockDb =
    let mockit =
        let mkey = Utils.makeKey
        let mkUser (keyNum, nick) = { Key = (mkey keyNum); Nick = nick }
        let mkId = Guid.NewGuid()
        let mkWallet user coin amount: WalletTbl =
            { Id = mkId; UserKey = user.Key; Coin = coin; Amount = amount }
        
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
        writeToFile json |> ignore

