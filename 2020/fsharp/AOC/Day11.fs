module AOC.Day11 

let directions = [|(-1, 0); (1, 0); (0, 1); (0, -1);
                  (-1, -1); (-1, 1); (1, -1); (1, 1)|]

let stepSim counter tolerance board = 
    Array2D.mapi (fun row col ele -> 
                  if ele = 'L' && (counter board row col) = 0 then
                    '#'
                  elif ele = '#' && (counter board row col) >= tolerance then
                    'L'
                  else ele) board

let countHash board = 
    board |> Seq.cast<char> |> Seq.filter ((=) '#') |> Seq.length

let runSim counter tolerance board = 
    let rec inner starting ending prev =
        if (starting - ending) = 0 then ending 
        else 
            let next = stepSim counter tolerance prev 
            inner ending (countHash next) next
    inner 1 (countHash board) board

let checkBounds row col board =
    ((row < Array2D.length1 board) && (row >= 0)
    && (col < Array2D.length2 board) && (col >= 0))

let part1 data = 
    let counter board row col =
        directions
        |> Seq.filter (fun (offrow, offcol) -> 
                    let (newrow, newcol) = (row + offrow, col + offcol)
                    if checkBounds newrow newcol board then board.[newrow, newcol] = '#'
                    else false)
        |> Seq.length
    runSim counter 4 data

let runLength (row, col) board (offrow, offcol) =
    let rec inner distance = 
        let newrow = offrow * distance + row
        let newcol = offcol * distance + col
        if not (checkBounds newrow newcol board) then false 
        elif board.[newrow, newcol] = 'L' then false 
        elif board.[newrow, newcol] = '#'  then true
        else inner (distance + 1)
    inner 1

let part2 data = 
    let counter board row col = 
        directions 
        |> Seq.filter (runLength (row, col) board)
        |> Seq.length
    runSim counter 5 data
