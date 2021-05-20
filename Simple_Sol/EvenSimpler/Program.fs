open System
open Models.Dtos
open Models.Common
open System.IO

[<EntryPoint>]
let main argv =
    let transactionDto: TransactionDto = {
        Id = Guid.NewGuid()
        SenderKey = 127
        RecieverKey = 450
        Signature = None
        Amount = 0.01
        Coin = Coin.Bitcoin
    }

    printfn "%s" (Path.Combine(__SOURCE_DIRECTORY__, "coins.db3"))

    
    0 // return an integer exit code
