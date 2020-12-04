#![allow(dead_code)]

mod parsers;

fn day1_part1(data: &[i64]) -> i64 {
    for left in 0..data.len() {
        for right in left..data.len() {
            if data[left] + data[right] == 2020 {
                return data[left] * data[right];
            }
        }
    }
    0
}

fn day1_part2(data: &[i64]) -> i64 {
    for left in 0..data.len() {
        for center in left..data.len() {
            for right in center..data.len() {
                if data[left] + data[right] + data[center] == 2020 {
                    return data[left] * data[right] * data[center];
                }
            }
        }
    }
    0
}

fn extract_psk_and_rule(string: &str) -> (usize, usize, char, String) {
    let re = regex::Regex::new(r"(\d+)-(\d+) ([a-z]): (.+)").unwrap();
    let cap = re.captures(string).unwrap();
    (
        cap[1].parse().unwrap(),
        cap[2].parse().unwrap(),
        cap[3].chars().nth(0).unwrap(),
        cap[4].into(),
    )
}

fn day2_part1(data: &[String]) -> usize {
    data.iter()
        .filter(|line| {
            let (min, max, letter, word) = extract_psk_and_rule(line);
            let letter_count = word.chars().filter(|x| *x == letter).count();
            letter_count >= min && letter_count <= max
        })
        .count()
}

fn day2_part2(data: &[String]) -> usize {
    data.iter()
        .filter(|line| {
            let (first, last, letter, word) = extract_psk_and_rule(line);
            (word.chars().nth(first - 1).unwrap() == letter)
                != (word.chars().nth(last - 1).unwrap() == letter)
        })
        .count()
}

fn count_trees(slope: &(usize, usize), data: &[Vec<char>]) -> u64 {
    let mut pos = [0, 0];
    let (length, width) = (data.len(), data[0].len());
    let mut count = 0;
    while pos[0] < length {
        count += if data[pos[0]][pos[1] % width] == '#' { 1 } else { 0 };
        pos[0] += slope.0;
        pos[1] += slope.1;
    }
    count
}

fn day3_part1(data: &[Vec<char>]) -> u64 {
    count_trees(&(1, 3), data)
}

fn day3_part2(data: &[Vec<char>]) -> u64 {
    [(1usize, 1usize), (1, 3), (1, 5), (1, 7), (2, 1)]
        .iter()
        .fold(1, |accum, slope| accum * count_trees(slope, data))
}

#[cfg(test)]
mod tests {
    #[test]
    fn day1_part1() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(514579, crate::day1_part1(&testdata));
        assert_eq!(842016, crate::day1_part1(&crate::parsers::day1()));
    }

    #[test]
    fn day1_part2() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(241861950, crate::day1_part2(&testdata));
        assert_eq!(9199664, crate::day1_part2(&crate::parsers::day1()));
    }

    #[test]
    fn day2_part1() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(2, crate::day2_part1(&testdata));
        assert_eq!(603, crate::day2_part1(&crate::parsers::day2()));
    }

    #[test]
    fn day2_part2() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(1, crate::day2_part2(&testdata));
        assert_eq!(404, crate::day2_part2(&crate::parsers::day2()));
    }

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
        assert_eq!(7, crate::day3_part1(&testdata));
        assert_eq!(265, crate::day3_part1(&crate::parsers::day3()));
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
        assert_eq!(336, crate::day3_part2(&testdata));
        assert_eq!(3154761400, crate::day3_part2(&crate::parsers::day3()));
    }
}
