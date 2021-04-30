module ValidationCommon

type Result<'a> =
    | Success of 'a
    | Failure of string list


let map f xResult =
    match xResult with
    | Success x -> Success (f x)
    | Failure errs -> Failure errs


let apply aResult bResult =
    match aResult, bResult with
    | Success a, Success b -> Success (a b)
    | Failure aErrs, Success _ -> Failure aErrs
    | Success _, Failure bErrs -> Failure bErrs
    | Failure aErrs, Failure bErrs -> Failure (List.concat [aErrs; bErrs])


let bind f aResult =
    match aResult with
    | Success a -> f a
    | Failure aErrs -> Failure aErrs


let (<!>) = bind
let (<*>) = apply


let zip a b =
    let toTuple a b = (a, b)
    Success toTuple <*> a <*> b
    // Success (toTuple (apply a apply b))
    // why order of ops must be specified here ???
