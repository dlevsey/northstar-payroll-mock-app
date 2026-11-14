import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    // VM workers are reliable in WSL/CI environments where spawning Vitest's
    // worker-thread bootstrap can stall before test collection.
    pool: 'vmThreads',
    maxWorkers: 1,
    fileParallelism: false,
    testTimeout: 15_000,
    hookTimeout: 15_000,
  },
});
