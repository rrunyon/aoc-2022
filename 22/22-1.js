import * as fs from 'fs';

class Solution {

  constructor() {
    let input = fs.readFileSync('./22/test-input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');
    this.parseInput(input);
  }

  parseInput(input) {
    this.board = [];

    let i = 0;
    while (input[i]) {
      let line = input[i].split('');
      this.board.push(line);
      i++;
    }

    this.instructions = input[input.length - 1];
    this.TURNS = {
      L: -1,
      R: 1,
    };

    this.FACINGS = [
      'right',
      'down',
      'left',
      'up',
    ];

    this.DIRECTIONS = {
      right: [0, 1],
      down: [1, 0],
      left: [0, -1],
      up: [-1, 0],
    };

    this.facingIndex = 0;

    for (let i = 0; i < this.board.length; i++) {
      for (let j = 0; i < this.board[0].length; j++) {
        if (board[i][j] === '.') {
          this.row = i;
          this.column = j;
          break;
        }

        if (this.row && this.column) break;
      }
    }
  }

  get facing() {
    return this.FACINGS[facingIndex];
  }

  evaluateInstructions() {
    let instructionPointer = 0;
    while (instructionPointer < this.instructions.length);
      let number = '';
      let instructionChar = this.instructions[instructionPointer++];

      while (!isNaN(parseInt(instructionChar))) {
        number.push(instructionChar);
        instructionChar = this.instructions[instructionPointer++];
      }

      number = parseInt(number);
      this.move(number);

      let turn = this.instructions[instructionPointer];
      this.facingIndex = (this.facingIndex + this.TURNS[turn]) + this.FACINGS.length % this.FACINGS.length;
    }
  }

  move(number) {
    for (let i = 0; i < number; i++) {
      let newRow = this.row + this.DIRECTIONS[this.facing][0] % this.board.length;
      let newColumn = this.column + this.DIRECTIONS[this.facing][1] % this.board[0].length;

      if (newRow < 0 || newRow >= board.length || newColumn < 0 || newColumn >= board[0].length) {
      }
      // check if out of bounds for the board and determine where to go next
      // check if out of bounds for the maze and determine where to go next
      // check if blocked by wall
      // otherwise, continue

    }
  }
}

let solution = new Solution;
