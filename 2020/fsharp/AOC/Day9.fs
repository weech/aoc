module AOC.Day9

let checkValid preamble wanted = 
    let rec innerTest left right = 
        if right >= Array.length preamble then false
        elif preamble.[left] > wanted then false
        elif (preamble.[left] + preamble.[right]) = wanted then true
        else innerTest left (right+1)
    let rec outerTest left =
        if left > (Array.length preamble - 1) then false
        elif innerTest left (left+1) then true 
        else outerTest (left+1)
    outerTest 0

let part1 (data: int64 []) lenPreamble = 
    let rec loop idx = 
        if idx > Array.length data then invalidArg "data" "No bad number"
        elif not (checkValid data.[(idx - lenPreamble)..(idx-1)] data.[idx]) then data.[idx]
        else loop (idx + 1)
    loop lenPreamble

let part2 data lenPreamble = 
    let target = part1 data lenPreamble
    let rec widenWindow left right = 
        let total = Array.sum data.[left..right]
        if total = target then 
            Some ((Array.min data.[left..right]) + (Array.max data.[left..right]))
        elif total > target then None 
        else widenWindow (left-1) right

    let rec walkLeft right =
        if right < 1 then invalidArg "data" "No summing section"
        else match widenWindow (right-1) right with 
                | Some v -> v 
                | None -> walkLeft (right-1)
    
    walkLeft ((Array.length data) - 1)
