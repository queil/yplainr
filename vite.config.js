import { defineConfig } from 'vite'

export default defineConfig({
  root: "src/client",
  build: {
    outDir: "../../public",
    emptyOutDir: true,
    sourcemap: true,
    rolloutOptions: {
      external: [
        'd3-scale',
        'd3-hierarchy'
      ]
    }
  }
});
