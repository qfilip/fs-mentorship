module ValidationCommon

type Result<'a> =
    | Success of 'a
    | Failiure of string list


let map f xResult =
    match xResult with
    | Success x -> Success (f x)
    | Failiure errs -> Failiure errs


let apply aResult bResult =
    match aResult, bResult with
    | Success a, Success b -> Success (a b)
    | Failiure aErrs, Success _ -> Failiure aErrs
    | Success _, Failiure bErrs -> Failiure bErrs
    | Failiure aErrs, Failiure bErrs -> Failiure (List.concat [aErrs; bErrs])


let bind f aResult =
    match aResult with
    | Success a -> f a
    | Failiure aErrs -> Failiure aErrs


let (<!>) = bind
let (<*>) = apply


let zip a b =
    let toTuple a b = (a, b)
    Success toTuple <*> a <*> b
    // Success (toTuple (apply a) (apply b))
    // why order of ops must be specified here ???