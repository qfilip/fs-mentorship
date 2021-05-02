module Models

open System
open WrappedTypes.Sha0


module Common =
    type Coin = Bitcoin | Ethereum | Litecoin
    

open Common
module Tables = 
    type UserTbl = {
        Key: int
        Nick: string
    }

    type WalletTbl = {
        Id: Guid
        UserKey: int
        Coin: Coin
        Amount: float
    }

    type TransactionTbl = {
        Id: Guid
        Sender: int
        Reciever: int
        Signature: int
        Amount: float
        Coin: Coin
    }


module Entities =
    type Wallet = {
        Id: Guid
        UserKey: Sha0
        Coin: Coin
        Amount: float
    }

    type User = {
        Key: Sha0
        Nick: string
        Wallet: Wallet option
    }

    type Transaction = {
        Id: Guid
        SenderKey: Sha0
        RecieverKey: Sha0
        Signature: Sha0
        Amount: float
        Coin: Coin
    }


module Dtos =
    
    type WalletDto = {
        Id: Guid
        UserKey: int
        Coin: Coin
        Amount: float
    }

    type UserDto = {
        Key: int
        Nick: string
        Wallet: WalletDto option
    }

    type TransactionDto = {
        Id: Guid
        SenderKey: int
        RecieverKey: int
        Signature: int option
        Amount: float
        Coin: Coin
    }


module DomainErrors =
    type InvalidSignature = string
    type InsufficientFunds = string
    type InvalidCoinType = string
    type InvalidKey = string
    
    type TransactionError =
        | InvalidKey
        | InvalidSignature
        | InvalidCoinType
        | InsufficientFunds


module FunctionTypes =
    open Dtos
    open Entities
    open DomainErrors

    type ValidateTransaction = TransactionDto -> Result<Transaction, TransactionError>
