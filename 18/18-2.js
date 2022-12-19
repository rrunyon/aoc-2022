import * as fs from 'fs';
import { Queue } from '@datastructures-js/queue';

const dirs = [
  [0, 0, 1],
  [0, 0, -1],
  [0, 1, 0],
  [0, -1, 0],
  [1, 0, 0],
  [-1, 0, 0],
];

function measureExternalDropletSurfaceArea() {
  let input = fs.readFileSync('./18/input.txt', { encoding: 'utf8', flag: 'r' }).split('\n');
  let droplet = new Set;
  let minX = Infinity;
  let minY = Infinity;
  let minZ = Infinity;
  let maxX = -Infinity;
  let maxY = -Infinity;
  let maxZ = -Infinity;
  
  for (let line of input) {
    let [x, y, z] = line.split(',').map(i => parseInt(i));
    minX = Math.min(minX, x);
    minY = Math.min(minY, y);
    minZ = Math.min(minZ, z);
    maxX = Math.max(maxX, x);
    maxY = Math.max(maxY, y);
    maxZ = Math.max(maxZ, z);
    droplet.add(line);
  }

  let queue = new Queue;
  let visited = new Set;
  let initialPoint = `${minX - 1},${minY - 1},${minZ - 1}`;
  queue.enqueue(initialPoint);
  visited.add(initialPoint); 

  let exposedFaces = 0;

  while (queue.size()) {
    let point = queue.dequeue();
    let [x, y, z] = point.split(',').map(i => parseInt(i));

    for (let dir of dirs) {
      let newX = x + dir[0];
      let newY = y + dir[1];
      let newZ = z + dir[2];
      let newPoint = `${newX},${newY},${newZ}`;

      if (newX >= (minX - 1) && newX <= (maxX + 1) &&
          newY >= (minY - 1) && newY <= (maxY + 1) &&
          newZ >= (minZ - 1) && newZ <= (maxZ + 1) &&
          !visited.has(newPoint)) {

        if (droplet.has(newPoint)) {
          exposedFaces++;
        } else {
          visited.add(newPoint);
          queue.enqueue(newPoint);
        }
      }
    }
  }

  return exposedFaces;
}

console.log(measureExternalDropletSurfaceArea());
