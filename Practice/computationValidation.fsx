open System.Text.RegularExpressions

type PasswordValidator() = 

    member _.Bind(x, f) =
        Result.bind f x


    member _.MergeSource(e, es) =
        Result.mapError (fun x -> e @ es)

    // member _.Bind3(x) =
    //     Result.


    member _.Return(x) = Ok x


let passwaldor = PasswordValidator()

let vpass = "MyStupidP@ssword1"
let ipass = "passwd"

let toResult x bool errmsg = if bool then Ok x else Error [errmsg]

let hasDigits (x: string) = toResult x (Regex.IsMatch(x, "\d")) "Digits required"
let hasSymbol (x: string) = toResult x (Regex.IsMatch(x, "\W")) "Symbol required"
let hasLength (x: string) = toResult x (x.Length >= 8) "Too short"
let hasUppercase (x: string) = toResult x (Regex.IsMatch(x, "[A-Z]")) "No uppercases"

// How to failfast (monad)
// How to list all errors (applicative)

let validator x = 
    passwaldor {
        let! hd = hasDigits x
        and! hs = hasSymbol x
        return hd
    }


