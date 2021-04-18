open System.Text.RegularExpressions

type PasswordVeryfier() = 

    member _.Bind(x, f) =
        Result.bind f x


    // member _.Bind3(x) =
    //     Result.


    member _.Return(x) = Ok x


let pvfy = PasswordVeryfier()

let vpass = "MyStupidP@ssword1"
let ipass = "passwd"

let toResult x bool errmsg = if bool then Ok x else Error errmsg

let hasDigits (x: string) = toResult x (Regex.IsMatch(x, "\d")) "Digits required"
let hasSymbol (x: string) = toResult x (Regex.IsMatch(x, "\W")) "Symbol required"
let hasLength (x: string) = toResult x (x.Length >= 8) "Too short"
let hasUppercase (x: string) = toResult x (Regex.IsMatch(x, "[A-Z]")) "No uppercases"

// How to failfast (monad)
// How to list all errors (applicative)

let validator x = 
    pvfy {
        let! hd = hasDigits x
        return hd
    }

// let rec validate password validatorFns validity errors =
//     match validatorFns with
//     | [] ->
//         match errors with
//         | [] -> Ok password
//         | _ -> Error errors
//     | vFn::vFns ->
//         let currentState = vFn password
//         match currentState with
//         | Error err ->
//             let currentErrors = errors @ err
//             validate password vFns currentState currentErrors
//         | Ok _ -> validate password vFns 
