module Mapper

open WrappedTypes
open Models.Entities
open Models.Dtos
open ValidationCommon


module User =
    let (<!>) = bind
    let (<*>) = apply
    
    let private createUser key nick wallet =
        let entity: User = { Key = key; Nick = nick; Wallet = wallet }        
        entity
    
    // create with regular fields like Nick & Wallet (for now)
    let mapEntity (dto: UserDto) =
        match dto.Key |> Sha0.wrap with
        | Success key -> Success (createUser key dto.Nick dto.Wallet)
        | Failure fs -> Failure fs

        // dto -> domain -> persisted -> result /// make a type out of it
        


