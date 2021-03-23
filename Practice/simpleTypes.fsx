module SimpleTypes
    let nullMessage = "input is null"
    let invalidMessage = "input is not valid"

    let createString fieldName value ctor lengthValidator = 
        if isNull value then
            let msg = sprintf "%s must not be null or empty" fieldName
            failwith msg
        elif not (lengthValidator value) then
            let msg = sprintf "%s has incorrect length" fieldName
            failwith msg
        else
            Ok (ctor value)
    

    module String5 =
        type String5 = private String5 of string
        
        let create x =
            let validator x = String.length x |> (fun x -> x = 0)
            createString "String5" x String5 validator

        let getValue (String5 x) = x

    
    module String25 =
        type String25 = private String25 of string
        
        let create str =
            let validator str = String.length str |> (fun x -> 0 > x && x <= 25)
            createString "String25" str String25 validator

        let getValue (String25 str25) = str25


    module String100 =
        type String100 = private String100 of string
        
        let create str =
            let validator str = String.length str |> (fun x -> 0 > x && x <= 100)
            createString "String50" str String100 validator

        let getValue (String100 str100) = str100