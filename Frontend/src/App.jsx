import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import ProtectedRoute from './components/ProtectedRoute'
import DashboardLayout from './components/DashboardLayout'
import UserManagement from './pages/UserManagement'
import CandidateManagement from './pages/CandidateManagement'
import SkillManagement from './pages/SkillManagement'
import JobPositionManagement from './pages/JobPositionManagement'
import JobApplications from './pages/JobApplications'
import Unauthorized from './pages/Unauthorized'

function App() {

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginPage />} />
          <Route path='/unauthorized' element={<Unauthorized />} />

          <Route element={<ProtectedRoute allowedRoles={["ADMIN"]} />}>
            <Route path='/user' element={<DashboardLayout />}>
              <Route path='user-management' element={<UserManagement />} />
              <Route path='candidate-management' element={<CandidateManagement />} />
              <Route path='skill-management' element={<SkillManagement />} />
              <Route path='job-management' element={<JobPositionManagement />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>

    </>
  )
}

export default App
