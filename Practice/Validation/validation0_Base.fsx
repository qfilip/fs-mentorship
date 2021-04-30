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


let zip x y =
    let toTuple x1 x2 = (x1, x2)
    Success (toTuple (apply x) (apply y))