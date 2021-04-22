module WrappedTypes
open System

let private defMsg = "Failed to create"

let getValueError typeName error =
    sprintf "%s %s. Reason: %s" defMsg typeName error


let private getNullError typeName =
    sprintf "%s %s, the value is null" defMsg typeName


module Sha0 =
    type Sha0 = private Sha0 of string
    let private typeName = nameof(Sha0)
        
    let private validator x =
        let maxValue = int (Math.Pow(2., float (Utils.HashLength)))
        
        if x >= 0 && x <= maxValue then
            let error = sprintf "Must be within range: 0-%i" maxValue
            Error (getValueError typeName error)
        else
            let sha0 = Sha0 (Utils.makeKey x)
            Ok (sha0)
        
    let wrap x = validator x
        
    let unwrap (Sha0 x) = x
    
    let wrapUnchecked x = Sha0 x