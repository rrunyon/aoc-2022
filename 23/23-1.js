import * as fs from 'fs';

const directions = [
  {
    checks: [[-1, 0], [-1, 1], [-1, -1]],
    move: [-1, 0]
  },
  {
    checks: [[1, 0], [1, 1], [1, -1]],
    move: [1, 0]
  },
  {
    checks: [[0, -1], [-1, -1], [1, -1]],
    move: [0, -1]
  },
  {
    checks: [[0, 1], [1, 1], [-1, 1]],
    move: [0, 1]
  },
];

function solution() {
  let input = fs.readFileSync('./23/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');

  let elves = new Set;

  for (let i = 0; i < input.length; i++) {
    for (let j = 0; j < input[0].length; j++) {
      if (input[i][j] === '#') elves.add(JSON.stringify([i, j]))
    }
  }

  let moves = new Map;

  for (let i = 0; i < 10; i++) {
    console.log('-------------------------------');
    console.log("round: ", i);
    print(elves);

    for (let elf of elves) {
      let [y, x] = JSON.parse(elf);

      // No adjacent elves in the 8 directionally adjacent squares
      let doNothing = !directions.map(d => d.checks).flat().some(check => {
        let newY = y + check[0];
        let newX = x + check[1];
        let newKey = JSON.stringify([newY, newX]);
        return elves.has(newKey);
      });

      if (doNothing) continue;

      for (let direction of directions) {
        // No elves in the 3 directionally adjacent squares
        let noElves = !direction.checks.some(check => {
          let newY = y + check[0];
          let newX = x + check[1];
          let newKey = JSON.stringify([newY, newX]);
          return elves.has(newKey);
        });

        if (noElves) {
          let newY = y + direction.move[0];
          let newX = x + direction.move[1];
          let moveKey = JSON.stringify([newY, newX]);
          if (!moves.has(moveKey)) moves.set(moveKey, []);
          moves.get(moveKey).push(elf);
          break;
        }
      }
    }

    console.log(moves);
    for (let [newKey, proposedElves] of moves) {
      if (proposedElves.length === 1) {
        let [elf] = proposedElves;
        elves.delete(elf);
        elves.add(newKey);
      }
    }

    moves.clear();
    directions.push(directions.shift());
    console.log(elves.size);
  }

  let minX = Infinity;
  let minY = Infinity;
  let maxX = -Infinity;
  let maxY = -Infinity;

  for (let elf of elves) {
    let [y, x] = JSON.parse(elf);
    minX = Math.min(minX, x);
    minY = Math.min(minY, y);
    maxX = Math.max(maxX, x);
    maxY = Math.max(maxY, y);
  }

  let area = (maxX - minX + 1) * (maxY - minY + 1);
  return area - elves.size;
}

function print(elves) {
  console.log();
  let minX = Infinity;
  let minY = Infinity;
  let maxX = -Infinity;
  let maxY = -Infinity;

  for (let elf of elves) {
    let [y, x] = JSON.parse(elf);
    minX = Math.min(minX, x);
    minY = Math.min(minY, y);
    maxX = Math.max(maxX, x);
    maxY = Math.max(maxY, y);
  }

  for (let i = minY; i <= maxY; i++) {
    let row = [];
    for (let j = minX; j <= maxX; j++) {
      let key = JSON.stringify([i, j]);
      if (elves.has(key)) {
        row.push('#');
      } else {
        row.push('.');
      }
    }
    console.log(row.join(''));
  }
}

console.log(solution());
