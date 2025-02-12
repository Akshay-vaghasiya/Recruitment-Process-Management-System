import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { AuthProvider } from './contexts/AuthContext.jsx'
import { ToastContainer } from 'react-toastify'
import { UserProvider } from './contexts/UserContext.jsx'
import { CandidateProvider } from './contexts/CandidateContext.jsx'
import { SkillProvider } from './contexts/SkillContext.jsx'
import { JobPositionProvider } from './contexts/JobPositionContext.jsx'

createRoot(document.getElementById('root')).render(
  <>
    <AuthProvider>
      <UserProvider>
        <CandidateProvider>
          <SkillProvider>
            <JobPositionProvider>
              <App />
            </JobPositionProvider>
          </SkillProvider>
        </CandidateProvider>
      </UserProvider>
    </AuthProvider>
    <ToastContainer />
  </>
)
