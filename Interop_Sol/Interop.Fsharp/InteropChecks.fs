module InteropChecks

let nullToOption (a: 'a) = if isNull a then None else Some a
