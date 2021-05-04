module BusinessLogic

open Dal.Common
open ValidationCommon
open Models.Dtos
open Expressions
open Models.Tables

let dbResultBind f dbResult = 
    match dbResult with
    | Failure _ -> failwith "Database error"
    | Success result -> f result


let resultBind f result error =
    match result with
    | Failure _ -> failwith error
    | Success r -> f r


let checkUserExists (userKey: int) =
    let command (db: SampleDatabase) = 
        db.Users |> List.tryFind (fun x -> x.Key = userKey)
    
    let dbResult = (execute command)
    
    let parseResult x = function
    | None -> false
    | Some _ -> true
    
    (dbResultBind parseResult dbResult)


let getUsersByKeys (keys: int list) =
    let command (db: SampleDatabase) = 
        db.Users |> List.filter (fun x -> keys |> List.contains x.Key)

    let dbResult = execute command
    let parseResult x = Success x

    (dbResultBind parseResult dbResult)


let getWalletByUserKey key =
    let command (db: SampleDatabase) = 
        db.Wallets |> List.find (fun x -> x.UserKey = key)

    let dbResult = execute command
    let parseResult x = Success x

    (dbResultBind parseResult dbResult)


let validateTransactionRequest (dto: TransactionDto) =
    // validate amount > 0
    // get users
    // user list = 2
    // sender wallet has >= amount

    let keys = [dto.SenderKey; dto.RecieverKey]
    let amountValidator x = if x > 0. then Success x else Failure ["Amount must be bigger than 0"]
    let usersValidator us = us |> List.length = 2
    

    let validator = ValidatorExpression()

    validator {
        let! amount = 
            if dto.Amount > 0.
            then Success dto.Amount
            else Failure ["Amount must be bigger than 0"]
        
        let! users = getUsersByKeys keys
        let! senderWallet = getWalletByUserKey dto.SenderKey
        
        let! hasMoney =
            if senderWallet.Amount > 0. && senderWallet.Amount <= amount
            then Success true
            else Failure ["Insuficcient funds"]



        return 0
    }
    0