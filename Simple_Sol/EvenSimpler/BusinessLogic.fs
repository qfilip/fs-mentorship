module BusinessLogic

open Dal.Common
open ValidationCommon
open Models.Dtos

let checkUserExists (user: UserDto) =
    let findUser (db: SampleDatabase) = 
        db.Users |> List.tryFind (fun x -> x.Key = user.Key)
    
    let dbResult = (execute findUser)
    match dbResult with
    | Failure fs -> fs
    | Success res ->
        match res with
        | None -> [""]
        | Some u -> [""] // ??? How to return true/false or list of errors


let executeTransaction (x: TransactionDto) =
    0