open System.Text.RegularExpressions

module ValidationResult =
    type ValidationResult<'a> =
        | Success of 'a
        | Fail of string list

    let map f xResult =
        match xResult with
        | Success x -> Success (f x)
        | Fail errs -> Fail errs


    let apply fResult xResult =
        match fResult, xResult with
        | Success f, Success x -> Success (f x)
        | Fail errs, Success x -> Fail errs
        | Success f, Fail errs -> Fail errs
        | Fail errs1, Fail errs2 -> Fail (List.concat [errs1; errs2])


    let bind f xResult =
        match xResult with
        | Success x -> f x
        | Fail errs -> Fail errs


open ValidationResult

type Id = Id of int
type Email = Email of string
type User = { Id: Id; Email: Email }


let createId id =
    match (id > 0) with
    | true -> Success (Id id)
    | _ -> Fail ["Id must be above 0"]


let validateEmail str = 
    match Regex.IsMatch(str, "@") with
    | true -> Success (Email str)
    | _ -> Fail ["Bad email pattern"]
    

let createEmail str =
    if isNull str then Fail ["Email cannot be null"]
    else validateEmail str


let createUser id email =
    { Id = id; Email = email }


let (<!>) = ValidationResult.map
let (<*>) = ValidationResult.apply


let tryCreateUser id email =
    let idResult = createId id
    let emailResult = createEmail email
    createUser
    <!> idResult
    <*> emailResult



