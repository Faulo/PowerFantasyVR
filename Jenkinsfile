node('unity && windows') {
    checkout scm
    
    unityProject {
    	// define unity project location relative to repository
        LOCATION = ''
        
        // use auto-versioning based on tags+commits
        AUTOVERSION = 'git'
        
        // which Unity Test Runner modes to execute
        TEST_MODES = 'EditMode PlayMode'
        
        // which executables to create
        BUILD_FOR_WINDOWS = '1'
        BUILD_FOR_LINUX = '0'
        BUILD_FOR_MAC = '0'
        BUILD_FOR_WEBGL = '0'
        BUILD_FOR_ANDROID = '0'
    }
}

