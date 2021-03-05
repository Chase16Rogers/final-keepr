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

  async getKeepById(id) {
    const res = await api.get('api/keeps/' + id)
    return res.data
  }

  async createKeep(data) {
    const res = await api.post('api/keeps', data)
    AppState.keeps.push(res.data)
  }

  async addKeepView(id) {
    const currentKeep = await this.getKeepById(id)
    currentKeep.views += 1
    await api.put('api/keeps/' + id + '/update', currentKeep)
  }

  async addKeepCount(id) {
    const currentKeep = await this.getKeepById(id)
    currentKeep.keeps += 1
    await api.put('api/keeps/' + id + '/update', currentKeep)
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    const index = AppState.keeps.findIndex(k => k.id === id)
    AppState.keeps.splice(index, 1)
  }
}
export const keepsService = new KeepsService()
