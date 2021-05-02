module Mapper

open WrappedTypes
open Models.Entities
open Models.Dtos
open ValidationCommon


module Wallet =
    let private createWallet id userKey coin amount =
        let entity: Wallet = { Id = id; UserKey = userKey; Coin = coin; Amount = amount }
        entity

    let mapEntity (dto: WalletDto) =
        match dto.UserKey |> Sha0.wrap with
        | Failure fs -> Failure fs
        | Success userKey ->
            Success (createWallet dto.Id userKey dto.Coin dto.Amount)


module User =
    let private createUser key nick wallet =
        let entity: User = { Key = key; Nick = nick; Wallet = wallet }        
        entity
   

    let mapKey dtoKey =
        match dtoKey |> Sha0.wrap with
        | Failure fs -> Failure fs
        | Success key -> Success key


    let mapEntity (dto: UserDto) =
        match dto.Key |> Sha0.wrap with
        | Failure fs -> Failure fs
        | Success key ->
            match dto.Wallet with
            | None -> Success (createUser key dto.Nick None)
            | Some wDto ->
                match  Wallet.mapEntity wDto with
                | Failure fs -> Failure fs
                | Success wallet -> Success (createUser key dto.Nick (Some wallet))