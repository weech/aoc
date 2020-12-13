module AOC.Day12 

open System
open System.Numerics

let parseInstruction (instruction: string) =
    match List.ofSeq instruction with 
    | head::tail -> (head, float (String.Concat(Array.ofList tail)))
    | _ -> invalidArg "instruction" "instruction is too short"

// For some reason you can't multiply float with Complex in compiled code
let stretch (f: float) (c: Complex) = Complex(f, 0.) * c

let part1 data = 
    let move ((position, direction):(Complex * Complex)) ((action, number):(char * float)) = 
        match action with 
        | 'N' -> (position + (Complex(0., number)), direction)
        | 'S' -> (position + (Complex(0., -number)), direction)
        | 'E' -> (position + (Complex(number, 0.)), direction)
        | 'W' -> (position + (Complex(-number, 0.)), direction)
        | 'L' -> (position, direction * (Complex(0., 1.))**(number / 90.))
        | 'R' -> (position, direction * (Complex(0., -1.))**(number / 90.))
        | 'F' -> (position + (stretch number direction), direction)
        | _ -> invalidArg "action" "Not a valid action"
    let (position, _) = data
                        |> Seq.map parseInstruction
                        |> Seq.fold (move) (Complex(0., 0.), Complex(1., 0.))
    (abs position.Imaginary) + (abs position.Real)

let part2 data = 
    let move ((position, waypoint):(Complex * Complex)) ((action, number):(char * float)) = 
        match action with 
        | 'N' -> (position, waypoint + (Complex(0., number)))
        | 'S' -> (position, waypoint + (Complex(0., -number)))
        | 'E' -> (position, waypoint + (Complex(number, 0.)))
        | 'W' -> (position, waypoint + (Complex(-number, 0.)))
        | 'L' -> (position, waypoint * (Complex(0., 1.))**(number / 90.))
        | 'R' -> (position, waypoint * (Complex(0., -1.))**(number / 90.))
        | 'F' -> (position + (stretch number waypoint), waypoint)
        | _ -> invalidArg "action" "Not a valid action"
    let (position, _) = data
                        |> Seq.map parseInstruction
                        |> Seq.fold (move) (Complex(0., 0.), Complex(10., 1.))
    (abs position.Imaginary) + (abs position.Real)