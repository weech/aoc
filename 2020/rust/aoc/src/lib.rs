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

#[cfg(test)]
mod tests {
    #[test]
    fn day1_part1() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(crate::day1_part1(&testdata), 514579);
        assert_eq!(crate::day1_part1(&crate::parsers::day1()), 842016);
    }

    #[test]
    fn day1_part2() {
        let testdata = [1721, 979, 366, 299, 675, 1456];
        assert_eq!(crate::day1_part2(&testdata), 241861950);
        assert_eq!(crate::day1_part2(&crate::parsers::day1()), 9199664);
    }

    #[test]
    fn day2_part1() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(crate::day2_part1(&testdata), 2);
        //assert_eq!(crate::day2_part1(&crate::parsers::day2()), 603);
    }

    #[test]
    fn day2_part2() {
        let testdata = [
            "1-3 a: abcde".to_string(),
            "1-3 b: cdefg".to_string(),
            "2-9 c: ccccccccc".to_string(),
        ];
        assert_eq!(crate::day2_part2(&testdata), 1);
        //assert_eq!(crate::day2_part2(&crate::parsers::day2()), 404);
    }
}
