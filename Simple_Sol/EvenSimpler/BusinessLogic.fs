module BusinessLogic

open Dal.Common
open Models.Dtos
open Expressions
open Models.Tables
open Models.Entities
open WrappedTypes

let dbResultBind f dbResult = 
    match dbResult with
    | Ok result -> f result
    | Error _ -> failwith "Database error"


let resultBind f result error =
    match result with
    | Ok r -> f r
    | Error _ -> failwith error


module User =
    let checkUserExists (userKey: int) =
        let command (db: SampleDatabase) = 
            db.Users |> List.tryFind (fun x -> x.Key = userKey)
        
        let dbResult = (execute command)
        
        let parseResult x = function
        | None -> false
        | Some _ -> true
        
        (dbResultBind parseResult dbResult)


    let getByKey key =
        let command (db: SampleDatabase) =
            db.Users |> List.tryFind (fun x -> x.Key = key)

        let dbResult = execute command
        let parseResult x = Result.fromOption x

        (dbResultBind parseResult dbResult)


    let getByKeys (keys: int list) =
        let command (db: SampleDatabase) = 
            db.Users |> List.filter (fun x -> keys |> List.contains x.Key)

        let dbResult = execute command
        let parseResult x = Ok x

        (dbResultBind parseResult dbResult)


module Wallet =
    let getByUserKey key =
        let command (db: SampleDatabase) = 
            db.Wallets |> List.find (fun x -> x.UserKey = key)

        let dbResult = execute command
        let parseResult (x: WalletTbl): Wallet =
            let key = x.UserKey |> Utils.makeKey |> Sha0.wrapUnchecked
            { Id = x.Id; UserKey = key; Coin = x.Coin; Amount = x.Amount }

        (dbResultBind parseResult dbResult)


module Transaction = 
    let validateTransactionRequest (dto: TransactionDto) =
        let keys = [dto.SenderKey; dto.RecieverKey]
        let usersValidator us = us |> List.length = 2
        let amountValidator x maxAmount =
            if x > 0. && x <= maxAmount
            then Ok x
            else Error ["Insuficcient funds"]

        let hashKey = Utils.makeKey >> Sha0.wrapUnchecked

        let validator = ValidatorExpression()

        validator {
            let! sender = User.getByKey dto.SenderKey
            let! reciever = User.getByKey dto.RecieverKey
            let! users = User.getByKeys keys
            let! senderWallet = Wallet.getByUserKey dto.SenderKey
            let! amount = amountValidator dto.Amount senderWallet.Amount

            let transaction = {
                Id = dto.Id
                SenderKey = sender.Key
                RecieverKey = reciever.Key
                Amount = amount
                Coin = dto.Coin
            }

            return transaction
        }