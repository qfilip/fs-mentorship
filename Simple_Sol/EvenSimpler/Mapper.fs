module Mapper

open WrappedTypes
open Models.Entities
open Models.Dtos


module User =
    let (<!>) = ValidationCommon.bind
    let (<*>) = ValidationCommon.apply
    
    let private createUser key nick wallet = 
        let entity: User = { Key = key; Nick = nick; Wallet = wallet }
        entity
    
    // create with regular fields like Nick & Wallet (for now)
    let mapEntity (dto: UserDto) =
        let rKey = dto.Key |> Sha0.wrap
        createUser
        <!> (dto.Key |> Sha0.wrap)
        dto.Nick
        dto.Wallet
        