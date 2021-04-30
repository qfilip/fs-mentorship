module Models

open System
open WrappedTypes.Sha0


module Common =
    type Coin = Bitcoin | Ethereum | Litecoin
    
    type Wallet = {
        Id: Guid
        UserKey: Sha0
        Coin: Coin
        Amount: float
    }

open Common
module Tables = 
    type UserTbl = {
        Key: string
        Nick: string
    }

    type WalletTbl = {
        Id: Guid
        UserKey: string
        Coin: Coin
        Amount: float
    }

    type TransactionTbl = {
        Id: Guid
        Sender: string
        Reciever: string
        VerificationHash: string
        Coin: Coin
        Amount: float
    }


module Entities =
    type User = {
        Key: Sha0
        Nick: string
        Wallet: Wallet
    }

    type Transaction = {
        Id: Guid
        Sender: User
        Reciever: User
        VerificationHash: Sha0
        Exchange: Wallet
    }


module Dtos =
    open Entities

    type UserDto = {
        Key: int
        Nick: string
        Wallet: Wallet
    }

    type TransactionDto = {
        Id: Guid
        Sender: User
        Reciever: User
        Signature: int
        Exchange: Wallet
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

    type ValidateCrewMember = TransactionDto -> Result<Transaction, TransactionError>
