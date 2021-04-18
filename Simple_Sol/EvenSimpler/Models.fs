module Models
open WrappedTypes.YrsOfExp

type Role = Copilot | Pilot | Commander


type Plane =
    | Boeing of int
    | Airbus of string


type CrewMember = {
    Name: string
    YrsOfExp: YrsOfExp
    Role: Role
    Plane: Plane
}