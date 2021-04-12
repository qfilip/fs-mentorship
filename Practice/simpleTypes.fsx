module SimpleTypes
    let nullMessage = "input is null"
    let invalidMessage = "input is not valid"

    let wrapType fieldName value ctor validator =
        if (validator value) then
            Ok (ctor value)
        else
            Error (sprintf "%s has incorrect length" fieldName)
    
    let wrapNullableType fieldName value ctor lengthValidator = 
        if isNull value then
            Error (sprintf "%s must not be null or empty" fieldName)
        elif not (lengthValidator value) then
            Error (sprintf "%s has incorrect length" fieldName)
        else
            Ok (ctor value)
    

    module Age =
        type Age = private Age of int

        let wrap x =
            let validator x = x > 0 && x < 120
            wrapType "Age" x Age validator

        let unwrap (Age x) = x


    module String5 =
        type String5 = private String5 of string
        
        let wrap x =
            let validator x = String.length x |> (fun x -> x > 0)
            wrapNullableType "String5" x String5 validator

        let unwrap (String5 x) = x

    
    module String25 =
        type String25 = private String25 of string
        
        let wrap str =
            let validator str = String.length str |> (fun x -> 0 > x && x <= 25)
            wrapNullableType "String25" str String25 validator

        let unwrap (String25 str25) = str25


    module String100 =
        type String100 = private String100 of string
        
        let wrap str =
            let validator str = String.length str |> (fun x -> 0 > x && x <= 100)
            wrapNullableType "String50" str String100 validator

        let unwrap (String100 str100) = str100



