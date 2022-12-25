import * as fs from 'fs';
import { Queue } from '@datastructures-js/queue';

const directions = {
  '^': [-1, 0],
  '>': [0, 1],
  'v': [1, 0],
  '<': [0, -1],
  _: [0, 0],
};

function solution() {
  let input = fs.readFileSync('./24/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n').filter(l => !!l);
  let { blizzardPositions: currentBlizzardPositions, startPosition } = parseInput(input);
  let nextBlizzardPositions = new Map;
  let minute = 0;

  let queue = new Queue;
  queue.enqueue(startPosition);
  let visiting = new Set;
  visiting.add(startPosition);
  let stage = 0;

  while (queue.size()) {
    print(currentBlizzardPositions, visiting, input);
    for (let [position, blizzards] of currentBlizzardPositions) {
      let [currentY, currentX] = JSON.parse(position);

      for (let blizzard of blizzards) {
        let direction = directions[blizzard];
        let nextY = currentY + direction[0];
        let nextX = currentX + direction[1];

        if (input[nextY][nextX] === '#') {
          if (blizzard === '^') {
            nextY = input.length - 2;
          } else if (blizzard === '>') {
            nextX = 1;
          } else if (blizzard === 'v') {
            nextY = 1;
          } else if (blizzard === '<') {
            nextX = input[0].length - 2;
          }
        }

        let nextPosition = JSON.stringify([nextY, nextX]);
        if (!nextBlizzardPositions.has(nextPosition)) nextBlizzardPositions.set(nextPosition, []);
        nextBlizzardPositions.get(nextPosition).push(blizzard);
      }
    }
    currentBlizzardPositions = nextBlizzardPositions;
    nextBlizzardPositions = new Map;

    let size = queue.size();

    for (let i = 0; i < size; i++) {
      let position = queue.dequeue();

      visiting.delete(position);
      let [y, x] = JSON.parse(position);

      if (!currentBlizzardPositions.has(position)) {
        if (stage === 0 && y === input.length - 1) {
          stage++;
          visiting.clear();
          queue.clear();
          queue.enqueue(position);
        } else if (stage === 1 && y === 0) {
          stage++;
          visiting.clear();
          queue.clear();
          queue.enqueue(position);
        } else if (stage === 2 && y === input.length - 1) {
          return minute;
        }
      }

      for (let direction of Object.values(directions)) {
        let newY = y + direction[0];
        let newX = x + direction[1];
        let newKey = JSON.stringify([newY, newX]);

        if (!currentBlizzardPositions.has(newKey)) {
          if (input[newY] && input[newY][newX] !== '#' && !visiting.has(newKey)) {
            visiting.add(newKey);
            queue.enqueue(newKey);
          }
        }
      }
    }

    minute++;
  }
}

function parseInput(input) {
  let blizzardPositions = new Map;
  for (let i = 1; i < input.length - 1; i++) {
    for (let j = 1; j < input[0].length - 1; j++) {
      let char = input[i][j];

      if (directions[char]) {
        let key = JSON.stringify([i, j]);
        if (!blizzardPositions.has(key)) blizzardPositions.set(key, []);
        blizzardPositions.get(key).push(char)
      }
    }
  }

  let startPosition;
  for (let i = 0; i < input[0].length; i++) {
    if (input[0][i] === '.') {
      startPosition = JSON.stringify([0, i]);
      break;
    }
  }

  return { blizzardPositions, startPosition };
};

function print(positions, visiting, input) {
  console.log();

  for (let i = 0; i < input.length; i++) {
    let row = [];
    for (let j = 0; j < input[0].length; j++) {
      let key = JSON.stringify([i, j]);
      if (visiting.has(key)) {
        row.push('\x1b[31m' + 'E' + '\x1b[0m');
      } else if (positions.has(key)) {
        let blizzards = positions.get(key);
        if (blizzards.length === 1) {
          let [blizzard] = blizzards;
          // console.log('1 blizzard: ', blizzard);
          row.push(blizzard);
        } else if (blizzards.length > 1) {
          // console.log('multiple blizzard: ', blizzards.length);
          row.push(blizzards.length);
        }
      } else {
        if (j === 0 || i === 0 || j === input[0].length - 1 || i === input.length - 1) {
          row.push(input[i][j]);
        } else {
          row.push('.');
        }
      }
    }
    console.log(row.join(''));
  }
}

console.log(solution());
