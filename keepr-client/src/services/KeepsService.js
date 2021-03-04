import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAll() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getKeepsByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.keeps = res.data
  }

  async getKeepsByVault(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    AppState.keeps = res.data
  }

  async createKeep(data) {
    const res = await api.post('api/keeps', data)
    AppState.keeps.push(res.data)
  }
}
export const keepsService = new KeepsService()
