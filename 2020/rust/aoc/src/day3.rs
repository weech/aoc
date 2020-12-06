fn count_trees(slope: &(usize, usize), data: &[Vec<char>]) -> u64 {
    let mut pos = [0, 0];
    let (length, width) = (data.len(), data[0].len());
    let mut count = 0;
    while pos[0] < length {
        count += if data[pos[0]][pos[1] % width] == '#' {
            1
        } else {
            0
        };
        pos[0] += slope.0;
        pos[1] += slope.1;
    }
    count
}

fn part1(data: &[Vec<char>]) -> u64 {
    count_trees(&(1, 3), data)
}

fn part2(data: &[Vec<char>]) -> u64 {
    [(1usize, 1usize), (1, 3), (1, 5), (1, 7), (2, 1)]
        .iter()
        .fold(1, |accum, slope| accum * count_trees(slope, data))
}

#[cfg(test)]
mod tests {

    #[test]
    fn day3_part1() {
        let testdata = vec![
            vec!['.', '.', '#', '#', '.', '.', '.', '.', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '.', '.', '.', '#', '.', '.'],
            vec!['.', '#', '.', '.', '.', '.', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '.', '.', '.', '#', '.', '#'],
            vec!['.', '#', '.', '.', '.', '#', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '#', '.', '.', '.', '.', '.'],
            vec!['.', '#', '.', '#', '.', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '#'],
            vec!['#', '.', '#', '#', '.', '.', '.', '#', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '#', '.', '.', '.', '#', '.', '#'],
        ];
        assert_eq!(7, crate::day3::part1(&testdata));
        assert_eq!(265, crate::day3::part1(&crate::parsers::day3()));
    }
    #[test]
    fn day3_part2() {
        let testdata = vec![
            vec!['.', '.', '#', '#', '.', '.', '.', '.', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '.', '.', '.', '#', '.', '.'],
            vec!['.', '#', '.', '.', '.', '.', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '.', '.', '.', '#', '.', '#'],
            vec!['.', '#', '.', '.', '.', '#', '#', '.', '.', '#', '.'],
            vec!['.', '.', '#', '.', '#', '#', '.', '.', '.', '.', '.'],
            vec!['.', '#', '.', '#', '.', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '.', '.', '.', '.', '.', '.', '#'],
            vec!['#', '.', '#', '#', '.', '.', '.', '#', '.', '.', '.'],
            vec!['#', '.', '.', '.', '#', '#', '.', '.', '.', '.', '#'],
            vec!['.', '#', '.', '.', '#', '.', '.', '.', '#', '.', '#'],
        ];
        assert_eq!(336, crate::day3::part2(&testdata));
        assert_eq!(3154761400, crate::day3::part2(&crate::parsers::day3()));
    }
}