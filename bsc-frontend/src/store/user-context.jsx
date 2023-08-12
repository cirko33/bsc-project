import { createContext, useEffect, useState } from "react";
import { getToken, getUser } from "../services/userService";

const UserContext = createContext();

// eslint-disable-next-line react/prop-types
export const UserContextProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    updateUser();
  }, []);

  const updateUser = () => {
    if (getToken()) {
      getUser()
        .then((res) => setUser(res))
        .catch((err) => console.log(err));
    }
  };

  return (
    <UserContext.Provider
      value={{
        user,
        updateUser,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export default UserContext;
