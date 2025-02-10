import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import ProtectedRoute from './components/ProtectedRoute'
import DashboardLayout from './components/DashboardLayout'
import UserManagement from './pages/UserManagement'
import CandidateManagement from './pages/CandidateManagement'

function App() {

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginPage />} />

          <Route element={<ProtectedRoute />}>
            <Route path='/user' element={<DashboardLayout />}>
              <Route path='user-management' element={<UserManagement />} />
              <Route path='candidate-management' element={<CandidateManagement />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>

    </>
  )
}

export default App
