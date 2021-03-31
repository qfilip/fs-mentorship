module SimpleTypes

let private getValueError typeName =
    Error (sprintf "Failed to create %s, invalid value" typeName)

let private getNullError typeName =
    Error (sprintf "Failed to create %s, the value is null" typeName)


let wrap typeName value ctor validatorFn = 
        if (validatorFn value) then
            getValueError typeName
        else
            Ok (ctor value)


let wrapNullable typeName value ctor validatorFn =
    if (isNull value) then
        getNullError typeName
    elif (validatorFn value) then
        getValueError typeName
    else
        Ok (ctor value)
    

module Strength =
    type Strength = private Strength of int

    let private typeName = nameof(Strength)
    
    let private validator x = x >= 0 && x < 90
    
    let wrap x = wrap typeName x Strength validator
    
    let unwrap (Strength x) = x