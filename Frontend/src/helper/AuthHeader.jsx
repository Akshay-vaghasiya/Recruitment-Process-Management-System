const AuthHeader = () => {
    
    const token = localStorage.getItem('token');

    const headers = {
      'Content-Type': 'application/json', 
      'Authorization': `Bearer ${token}`, 
      'Access-Control-Allow-Origin': '*'
    };
    
    return token ? headers : {};
}

export default AuthHeader;
