class PolishService {
  randomHeights() {
    const rnd = Math.random() * 10
    return rnd.toString()
  }
}
export const polishService = new PolishService()
