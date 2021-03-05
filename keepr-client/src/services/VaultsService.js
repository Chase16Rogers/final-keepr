import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getVaultsByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.vaults = res.data
  }

  async getUsersVaults() {
    const id = AppState.account.id
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.usersVaults = res.data
  }

  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
  }

  async createVault(data) {
    const res = await api.post('api/vaults', data)
    AppState.vaults.push(res.data)
  }

  async deleteVault(id) {
    await api.delete('api/vaults/' + id)
    const index = AppState.vaults.findIndex(v => v.id === id)
    AppState.vaults.splice(index, 1)
  }
}
export const vaultsService = new VaultsService()
