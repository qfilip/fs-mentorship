module WrappedTypes

let private getValueError typeName error =
    Error (sprintf "Failed to create %s. Reason: %s" typeName error)


let private getNullError typeName =
    Error (sprintf "Failed to create %s, the value is null" typeName)


let wrap typeName value ctor validatorFn = 
    match (validatorFn value) with
    | Error err -> getValueError typeName err
    | Ok v -> Ok (ctor v)


let wrapNullable typeName value ctor validatorFn =
    match value with
    | null -> getNullError typeName
    | v -> wrap typeName v ctor validatorFn


module YrsOfExp =
    type YrsOfExp = private YrsOfExp of int
    let private typeName = nameof(YrsOfExp)
        
    let private validator x = 
        if x >= 1 && x < 30 then Ok x
        else Error "Invalid experience value"
        
    let wrap x = wrap typeName x validator
        
    let unwrap (YrsOfExp x) = x
    
    let wrapUnchecked x = YrsOfExp x