module AOC.Day10

let diff (arr: int []) = 
    Array.map2 (-) arr.[1..] arr.[..((Array.length arr) - 2)]

let countDigit digit arr = 
    arr |> Seq.filter ((=) digit) |> Seq.length

let part1 data =
    let differences = (Array.concat [[|0|]; data]) |> Array.sort |> diff 
    (countDigit 1 differences) * ((countDigit 3 differences) + 1)

let part2 data = 
    let differences = (Array.concat [[|0|]; data]) |> Array.sort |> diff 
    let groupLens = Array.fold (fun (groups: int []) d -> if d = 1 then 
                                                            let idx = (Array.length groups) - 1
                                                            groups.[idx] <- groups.[idx] + 1
                                                            groups
                                                          else Array.concat [groups; [|0|]])
                                [|0|]
                                differences
    let num2s = countDigit 2 groupLens
    let num3s = countDigit 3 groupLens
    let num4s = countDigit 4 groupLens
    (bigint 2)**num2s * (bigint 4)**num3s * (bigint 7)**num4s
