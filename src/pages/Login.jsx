import './Login.css'
import { useState } from "react";
import { FaEye, FaEyeSlash, FaFacebookF, FaGoogle, FaTwitter } from "react-icons/fa";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [keepSignedIn, setKeepSignedIn] = useState(false);
  const [loading, setLoading] = useState(false);
  const [errors, setErrors] = useState({ email: "", password: "" });

  const validateEmail = (email) => {
    const re = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return re.test(String(email).toLowerCase());
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
    if (!validateEmail(e.target.value)) {
      setErrors({ ...errors, email: "Please enter a valid email address" });
    } else {
      setErrors({ ...errors, email: "" });
    }
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
    if (e.target.value.length < 8) {
      setErrors({ ...errors, password: "Password must be at least 8 characters long" });
    } else {
      setErrors({ ...errors, password: "" });
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!errors.email && !errors.password) {
      setLoading(true);
      setTimeout(() => {
        setLoading(false);
        alert("Login successful!");
      }, 2000);
    }
  };

  return (
    <div className="login-container">
      <div className="login-background">
        <div>
          <h1 className="login-title">Oxyy</h1>
          <p className="login-subtitle">We are glad to see you again!</p>
        </div>
        {/* <div className="login-qr-container">
          <h2 className="login-qr-title">Log In with QR Code</h2>
          <div className="login-qr-placeholder">
          
          </div>
          <p className="login-qr-description">Scan this with your camera or our mobile app to login instantly</p>
        </div> */}
      </div>
      
      <div className="login-form-container">
        <h2 className="form-title">Sign-In</h2>
        <form onSubmit={handleSubmit} className="login-form">
          <div className="form-group">
            <label htmlFor="email" className="form-label">Email address</label>
            <input
              type="email"
              id="email"
              name="email"
              value={email}
              onChange={handleEmailChange}
              placeholder="Enter Your Email"
              className="form-input"
              required
              aria-describedby="email-error"
            />
            {errors.email && <p id="email-error" className="form-error">{errors.email}</p>}
          </div>
          <div className="form-group">
            <label htmlFor="password" className="form-label">Password</label>
            <div className="form-password">
              <input
                type={showPassword ? "text" : "password"}
                id="password"
                name="password"
                value={password}
                onChange={handlePasswordChange}
                placeholder="Enter Password"
                className="form-input"
                required
                aria-describedby="password-error"
              />
              <button
                type="button"
                className="password-toggle"
                onClick={() => setShowPassword(!showPassword)}
              >
                {showPassword ? <FaEyeSlash className="password-icon" /> : <FaEye className="password-icon" />}
              </button>
            </div>
            {errors.password && <p id="password-error" className="form-error">{errors.password}</p>}
          </div>
          <div className="form-group-checkbox">
            <input
              id="keep-signed-in"
              name="keep-signed-in"
              type="checkbox"
              checked={keepSignedIn}
              onChange={(e) => setKeepSignedIn(e.target.checked)}
              className="checkbox-input"
            />
            <label htmlFor="keep-signed-in" className="checkbox-label">Keep me signed in</label>
          </div>
          <div className="form-group">
            <button type="submit" className="submit-button" disabled={loading}>
              {loading ? (
                <svg className="loading-icon" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                  <circle className="loading-circle" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                  <path className="loading-path" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                </svg>
              ) : "Sign-In"}
            </button>
          </div>
        </form>
        <div className="social-login">
          <div className="social-divider">
            <span className="social-text">Or continue with</span>
          </div>
          <div className="social-buttons">
            <a href="a" className="social-button"><FaFacebookF className="social-icon facebook-icon" /></a>
            <a href="a" className="social-button"><FaGoogle className="social-icon google-icon" /></a>
            <a href="a" className="social-button"><FaTwitter className="social-icon twitter-icon" /></a>
          </div>
        </div>
        <p className="forgot-password">
          Forgot your <a href="a" className="forgot-link">username</a> or <a href="a" className="forgot-link">password</a>?
        </p>
      </div>
    </div>
  );
};

export default Login;
