import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import ProtectedRoute from './components/ProtectedRoute'
import DashboardLayout from './components/DashboardLayout'
import UserManagement from './pages/UserManagement'
import CandidateManagement from './pages/CandidateManagement'
import SkillManagement from './pages/SkillManagement'
import JobPositionManagement from './pages/JobPositionManagement'
import Unauthorized from './pages/Unauthorized'
import { Dashboard } from '@mui/icons-material'
import JobPositionCandidate from './pages/JobPositionCandidate'
import CandidateApplications from './pages/CandidateApplications'
import CandidateProfile from './pages/CandidateProfile'

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

          <Route element={<ProtectedRoute allowedRoles={["CANDIDATE"]} />}>
            <Route path='/candidate' element={<DashboardLayout />} >
              <Route path='job-positions' element={<JobPositionCandidate />} />
              <Route path='job-applications' element={<CandidateApplications />} />
              <Route path='profile' element={<CandidateProfile />} />
            </Route>
          </Route>

          <Route element={<ProtectedRoute allowedRoles={["RECRUITER"]} />}>
            <Route path='/recruiter' element={<DashboardLayout />} >
              <Route path='candidate-management' element={<CandidateManagement />} />
              <Route path='job-management' element={<JobPositionManagement />} />
            </Route>
          </Route>

          <Route element={<ProtectedRoute allowedRoles={["HR"]} />}>
            <Route path='/hr' element={<DashboardLayout />} >
              <Route path='candidate-management' element={<CandidateManagement />} />
              <Route path='job-management' element={<JobPositionManagement />} />
            </Route>
          </Route>

          <Route element={<ProtectedRoute allowedRoles={["REVIEWER"]} />}>
            <Route path='/reviewer' element={<DashboardLayout />} >
              <Route path='job-management' element={<JobPositionManagement />} />
            </Route>
          </Route>

          <Route element={<ProtectedRoute allowedRoles={["INTERVIEWER"]} />}>
            <Route path='/interviewer' element={<DashboardLayout />} >
              <Route path='job-management' element={<JobPositionManagement />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>

    </>
  )
}

export default App
