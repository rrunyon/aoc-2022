import * as fs from 'fs';

const dirs = [
  [0, 0, 1],
  [0, 0, -1],
  [0, 1, 0],
  [0, -1, 0],
  [1, 0, 0],
  [-1, 0, 0],
];

function measureDropletSurfaceArea() {
  let input = fs.readFileSync('./18/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');
  let droplet = new Set;

  for (let line of input) {
    droplet.add(line);
  }

  let exposedFaces = 0;
  for (let line of input) {
    let [x, y, z] = line.split(',').map(i => parseInt(i));

    for (let dir of dirs) {
      let newX = x + dir[0];
      let newY = y + dir[1];
      let newZ = z + dir[2];
      let plot = `${newX},${newY},${newZ}`;

      if (!droplet.has(plot)) {
        exposedFaces++;
      }
    }
  }

  return exposedFaces;
}

console.log(measureDropletSurfaceArea());
