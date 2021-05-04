module BusinessLogic

open Dal.Common
open ValidationCommon
open Models.Dtos

let onDbResult f dbResult = 
    match dbResult with
    | Failure _ -> failwith "Database error"
    | Success result -> f result


let checkUserExists (userKey: int) =
    let command (db: SampleDatabase) = 
        db.Users |> List.tryFind (fun x -> x.Key = userKey)
    
    let dbResult = (execute command)
    
    let parseResult x = function
    | None -> false
    | Some _ -> true
    
    (onDbResult parseResult dbResult)


let getUsersByKeys (keys: int list) =
    let command (db: SampleDatabase) = 
        db.Users |> List.filter (fun x -> keys |> List.contains x.Key)

    let dbResult = execute command
    let parseResult x = Success x

    (onDbResult parseResult dbResult)


let validateTransactionRequest (dto: TransactionDto) =
    let keys = [dto.SenderKey; dto.RecieverKey]
    let users = getUsersByKeys keys
        

    0