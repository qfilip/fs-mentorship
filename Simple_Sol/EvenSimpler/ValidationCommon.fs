[<RequireQualifiedAccess>]
module Result

let fromOption = function
    | Some x -> Ok x
    | None -> Error ["Data not found"]


let map f xResult =
    match xResult with
    | Ok x -> Ok (f x)
    | Error errs -> Error errs


let apply aResult bResult =
    match aResult, bResult with
    | Ok a, Ok b -> Ok (a b)
    | Error aErrs, Ok _ -> Error aErrs
    | Ok _, Error bErrs -> Error bErrs
    | Error aErrs, Error bErrs -> Error (List.concat [aErrs; bErrs])


let bind f aResult =
    match aResult with
    | Ok a -> f a
    | Error aErrs -> Error aErrs


let (<!>) = bind
let (<*>) = apply


let zip a b =
    let toTuple a b = (a, b)
    Ok toTuple <*> a <*> b
    // Ok (toTuple (apply a apply b))
    // why order of ops must be specified here ???
