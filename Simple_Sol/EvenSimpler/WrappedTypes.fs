module WrappedTypes

let private defMsg = "Failed to create"

let getValueError typeName error =
    sprintf "%s %s. Reason: %s" defMsg typeName error


let private getNullError typeName =
    sprintf "%s %s, the value is null" defMsg typeName


module Sha0 =
    type Sha0 = private Sha0 of string
    let private typeName = nameof(Sha0)
        
    let private validator (x: string) =
        if isNull x then Error (getNullError typeName)
        elif x.Length <> 4 then Error (getValueError typeName "Must be 4 chars long")
        else Ok (x)
        
    let wrap x = validator x
        
    let unwrap (Sha0 x) = x
    
    let wrapUnchecked x = Sha0 x