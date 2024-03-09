import { handleAuth } from "./handleAuth";
import { handleRootRoute } from "./handleRootRoute";

// Pipeline executes top to bottom
const middlewarePipeline = [
  handleAuth,
  handleRootRoute
];

export default middlewarePipeline;