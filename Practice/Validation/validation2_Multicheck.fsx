#load "validation0_Base.fsx"

open Validation0_Base
open System.Text.RegularExpressions

let toResult x bool errmsgs = if bool then Success x else Fail errmsgs

let hasDigits (x: string) = toResult x (Regex.IsMatch(x, "\d")) ["Digits required"]
let hasSymbol (x: string) = toResult x (Regex.IsMatch(x, "\W")) ["Symbol required"]
let hasLength (x: string) = toResult x (x.Length >= 8) ["Too short"]
let hasUppercase (x: string) = toResult x (Regex.IsMatch(x, "[A-Z]")) ["No uppercases"]

let (<!>) = Validation0_Base.bind
let (<*>) = Validation0_Base.apply

let createPassword a b c d = a

let validatePassword str =
    let hd = hasDigits str
    let hs = hasSymbol str
    let hl = hasLength str
    let hu = hasUppercase str
    createPassword
        <!> hd
        <*> hs
        <*> hl
        <*> hu

let vpass = "MyStupidP@ssword1"
let ipass = "passwd"



let result = validator ipass


